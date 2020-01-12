using BWYSDP.com;
using SDPCRL.COM.ModelManager.FormTemplate;

namespace BWYSDP.Controls
{
    public partial class FormGroupFieldProperty : BaseUserControl<LibFormGroupField>
    {
        //private LibFormGroupField _LibfmgroupField;
        private LibTreeNode _Node;
        public FormGroupFieldProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        public FormGroupFieldProperty(string name)
            : this()
        {
            this.Name = name;
        }
        public override void SetPropertyValue(LibFormGroupField entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }

        //public override void SetPropertyValue<TEntity>(TEntity entity, LibTreeNode node)
        //{
        //    base.SetPropertyValue<TEntity>(entity, node);
        //    _LibfmgroupField = entity as LibFormGroupField;
        //    _Node = node;
        //}
    }
}
