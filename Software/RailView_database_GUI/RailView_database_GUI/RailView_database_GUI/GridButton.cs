using System.Windows.Forms;

namespace RailView_database_GUI
{
    class GridButton
    {
        public string ButtonName { get; set; }

        public GridButton() : this("Standaard")
        {
        }

        public GridButton(string buttonName)
        {
            ButtonName = buttonName;
        }

        public DataGridViewButtonColumn SetButton()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "btn" + ButtonName;
            btn.HeaderText = ButtonName;
            btn.Text = ButtonName;
            btn.UseColumnTextForButtonValue = true;

            return btn;
        }
    }
}
