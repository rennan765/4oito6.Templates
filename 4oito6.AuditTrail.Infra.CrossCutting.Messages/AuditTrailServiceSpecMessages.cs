namespace _4oito6.AuditTrail.Infra.CrossCutting.Messages
{
    public static class AuditTrailServiceSpecMessages
    {
        public static string ExceptionNula { get; private set; }

        static AuditTrailServiceSpecMessages()
        {
            ExceptionNula = "A exceção é obrigatória.";
        }
    }
}