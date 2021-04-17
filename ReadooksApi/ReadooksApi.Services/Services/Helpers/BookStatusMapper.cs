using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.Services.Helpers
{
    public class BookStatusMapper
    {
        private static BookStatusMapper instance = null;

        private BookStatusMapper() { }

        public static BookStatusMapper GetInstance()
        {
            if (instance == null)
            {
                return new BookStatusMapper();
            }
            return instance;
        }

        public BookStatus Map(int status)
        {
            switch (status)
            {
                case 0:
                    return BookStatus.Open;
                case 1:
                    return BookStatus.Finished;
                case 2:
                    return BookStatus.Canceled;
                default:
                    return BookStatus.Open;
            }
        }

    }
}
