namespace _4oito6.Infra.Data.Model.Core
{
    public abstract class DataModelBase
    {
        public int Id { get; private set; }

        protected DataModelBase()
        {
        }

        protected DataModelBase(int id)
        {
            Id = id;
        }
    }
}