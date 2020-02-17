using System;

namespace _4oito6.Infra.CrossCutting.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsSameDay(this DateTime date, DateTime day) 
            => date.ToString("dd/MM/yyyy") == day.ToString("dd/MM/yyyy");
    }
}
