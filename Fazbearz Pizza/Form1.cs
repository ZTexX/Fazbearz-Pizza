namespace Fazbearz_Pizza
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            button1.Cursor = Cursors.Hand;
            button2.Cursor = Cursors.Hand;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}