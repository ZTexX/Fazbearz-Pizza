namespace Fazbearz_Pizza
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.LoginBtn.Click += new EventHandler(LoginBtn_Click);
            this.ExitBtn.Click += new EventHandler(ExitBtn_Click);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            LoginBtn.Cursor = Cursors.Hand;
            ExitBtn.Cursor = Cursors.Hand;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}