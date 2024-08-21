using _MVC_Identity_.Entities.Interface;

namespace _MVC_Identity_.Entities.Concreate
{
    public class Product : BaseEntity, IEntity<int>
    {
        public int ID { get ; set ; }
    }
}
