using System.Threading.Tasks;
namespace LogbookApp.Views
{
    public interface IMessager
    {
        Task ShowMessage(string message);
    }
}