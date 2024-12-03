// Buraya entities oluşturmamızın sebebi bir yapı kuracağız
// yetkilendirme yapısı ve her projeye entegre olabilecek bir sistem bu
namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
