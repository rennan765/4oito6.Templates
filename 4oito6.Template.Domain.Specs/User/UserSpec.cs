using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Template.Domain.Model.ValueObjects;
using _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs.User;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Entities = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Domain.Specs.User
{
    public class UserSpec : BusinessSpec
    {
        public UserSpec(Entities.User entity) : base(entity)
        {
            ValidateName(entity.Name);
            ValidateCpf(entity.Cpf);
            ValidateEmail(entity.Email);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ValidateEmail(string email)
        {
            if (!IsValidEmail(email))
            {
                AddMessage(BusinessSpecStatus.InvalidInputs, UserSpecMessages.EmailInvalido);
            }
        }

        private bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        private void ValidateCpf(string cpf)
        {
            if (!IsValidCpf(cpf))
            {
                AddMessage(BusinessSpecStatus.InvalidInputs, UserSpecMessages.CpfInvalido);
            }
        }

        private void ValidateName(Name name)
        {
            if (string.IsNullOrEmpty(name.FirstName))
                AddMessage(BusinessSpecStatus.InvalidInputs, UserSpecMessages.PrimeiroNomeObrigatorio);

            if (string.IsNullOrEmpty(name.LastName))
                AddMessage(BusinessSpecStatus.InvalidInputs, UserSpecMessages.SobrenomeObrigatorio);
        }
    }
}