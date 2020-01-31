using BWYSDP.com;
using SDPCRL.COM.ModelManager.FormTemplate;

namespace BWYSDP.Controls
{
    public partial class ButtonGroupProperty : BaseUserControl<LibButtonGroup>
    {
        private LibTreeNode _Node;
        public ButtonGroupProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
        public ButtonGroupProperty(string name)
        :this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibButtonGroup entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }
    }
}
