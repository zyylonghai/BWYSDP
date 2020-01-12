using BWYSDP.com;
using SDPCRL.COM.ModelManager.FormTemplate;

namespace BWYSDP.Controls
{
    public partial class FormGroupProperty : BaseUserControl<LibFormGroup>
    {
        //private LibFormGroup _LibformGroup;
        private LibTreeNode _Node;
        public FormGroupProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        public FormGroupProperty(string name)
            :this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibFormGroup entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }
        //public override void SetPropertyValue<TEntity>(TEntity entity, LibTreeNode node)
        //{
        //    base.SetPropertyValue<TEntity>(entity, node);
        //    _LibformGroup = entity as LibFormGroup;
        //    _Node = node;
        //}
    }
}
