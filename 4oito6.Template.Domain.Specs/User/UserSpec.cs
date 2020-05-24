using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Template.Domain.Model.ValueObjects;
using _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs.User;
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

        private bool IsValidEmail(string email) => Regex.IsMatch
        (
            input: email,
            pattern: @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            options: RegexOptions.IgnoreCase
        );

        private void ValidateEmail(string email)
        {
            if (!IsValidEmail(email))
            {
                AddMessage(BusinessSpecStatus.InvalidInputs, UserSpecMessages.EmailInvalido);
            }
        }

        private bool IsValidCpf(string cpf)
        {
            cpf = cpf.Trim();

            if (string.IsNullOrEmpty(cpf) || (cpf ?? string.Empty).Length != 11)
            {
                return false;
            }

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var tempCpf = cpf.Substring(0, 9);

            var sum = 0;
            int rest;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            }

            rest = (sum % 11) < 2 ? 0 : 11 - (sum % 11);

            var digit = rest.ToString();
            tempCpf += digit;

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            }

            rest = (sum % 11) < 2 ? 0 : 11 - (sum % 11);

            digit += rest.ToString();

            return cpf.EndsWith(digit);
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