using ViewModelLayer;
using ModelLayer;

namespace VierModelLayer
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private ModelAbstractAPI modelLayer = ModelAbstractAPI.createModelAPI;
        private int ballAmount;
    
    }


}