// Buraya entities oluşturmamızın sebebi bir yapı kuracağız
// yetkilendirme yapısı ve her projeye entegre olabilecek bir sistem bu
namespace Core.Entities.Concrete
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
