using Odontology.Web.Infrastructure;

namespace Odontology.Web.ViewModels
{
    public class EntityCreateViewModel<TViewModel> where TViewModel : class
    {
        public TViewModel EntityViewModel { get; set; }

        public ViewTypeEnum ViewType { get; set; }
    }
}
