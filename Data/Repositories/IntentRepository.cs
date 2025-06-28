using IntentManagementAPI.Models.Core;

namespace IntentManagementAPI.Data.Repositories
{
    public class IntentRepository : Repository<Intent>, IIntentRepository
    {
        public IntentRepository(IntentManagementContext context) : base(context)
        {
        }
        // Implement intent-specific methods here
    }
} 