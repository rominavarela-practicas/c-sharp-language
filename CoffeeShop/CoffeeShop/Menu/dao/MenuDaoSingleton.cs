using System.Threading.Tasks;

namespace CoffeeShop.Menu.dao
{
    public partial class MenuDao
    {
        static private Task _SingletonTask;
        static private MenuDao _Singleton;
        static public MenuDao Singleton
        {
            get
            {
                if (_Singleton == null)
                {
                    if (_SingletonTask == null)
                    {
                        _SingletonTask = new Task(() => { _Singleton = new MenuDao(); });
                        _SingletonTask.Start();
                    }
                    _SingletonTask.Wait();
                }
                return _Singleton;
            }
        }

        static private string _Path = "Menu.xml";
        static public void SetPath(string Path)
        {
            if (_Path == Path)
            {
                return;
            }

            if (_SingletonTask != null)
            {
                _SingletonTask.Wait();
            }

            _Path = Path;
            _Singleton = null;
            _SingletonTask = null;
        }
    }
}
