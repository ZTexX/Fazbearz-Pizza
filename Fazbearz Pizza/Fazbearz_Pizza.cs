using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Fazbearz_Pizza
{
    public partial class Fazbearz_Pizza : Form
    {
        private Panel[] panels;
        private int orderNum;
        private Model model;

        public Fazbearz_Pizza()
        {
            InitializeComponent();
            model = new Model();
        }

        private void Fazbearz_Pizza_Load(object sender, EventArgs e)
        {
            panels = Controls.OfType<Panel>().ToArray();
            SwitchMenu(MainMenuPanel);
        }

        private void SwitchMenu(Panel panel)
        {
            foreach (Panel p in panels)
            {
                if (p == panel)
                {
                    p.Show();
                } else
                {
                    p.Hide();
                }
            }
        }

        #region MainMenu
        //Main Menu BEGIN
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            SwitchMenu(LoginPanel);
        }

        private void ExitBtn_Click(Object sender, EventArgs e)
        {
            this.Close();
        }
        //Main Menu END
        #endregion

        #region Login

        //Log In Menu BEGIN

        private void BackBtn_Click(Object sender, EventArgs e)
        {
            SwitchMenu(MainMenuPanel);
        }

        private void CreateAccountBtn_Click(Object sender, EventArgs e)
        {
            SwitchMenu(CreateAccountPanel);
        }

        private void UsernameTxtBox_Enter(Object sender, EventArgs e)
        {
            if (UsernameTxtBox.Text == "Username")
            {
                UsernameTxtBox.Text = string.Empty;
            }
        }

        private void UserNameTxtBox_Leave(Object sender, EventArgs e)
        {
            if (UsernameTxtBox.Text == string.Empty)
            {
                UsernameTxtBox.Text = "Username";
            }
        }

        private void LoginPanel_Click(object sender, EventArgs e)
        {
            LoginPanel.Focus();
        }

        private void PasswordTxtBox_Enter(Object sender, EventArgs e)
        {
            if (PasswordTxtBox.Text == "Password")
            {
                PasswordTxtBox.Text = string.Empty;
                if (!ViewPasswordCheck.Checked)
                {
                    PasswordTxtBox.PasswordChar = '*';
                }
            }
        }

        private void PasswordTxtBox_Leave(Object sender, EventArgs e)
        {
            if (PasswordTxtBox.Text == string.Empty)
            {
                PasswordTxtBox.PasswordChar = '\0';
                PasswordTxtBox.Text = "Password";
            }
        }

        private void ViewPasswordCheck_Changed(Object sender, EventArgs e)
        {
            if (ViewPasswordCheck.Checked)
            {
                PasswordTxtBox.PasswordChar = '\0';
            } else if (PasswordTxtBox.PasswordChar == '\0' && PasswordTxtBox.Text != "Password")
            {
                PasswordTxtBox.PasswordChar = '*';
            }
        }

        private void ActualLoginBtn_Click(Object sender, EventArgs e)
        {
            string username = UsernameTxtBox.Text;
            string password = PasswordTxtBox.Text;
            bool validCustomerLogin = LogIn(username, password);
            
            if (validCustomerLogin)
            {
                SwitchMenu(OrderMenuPanel);

            }
            else if(model.IsManager)
                SwitchMenu(ManagerDatabasePanel);
            else
            {
                //bounce
            }

        }

        private bool LogIn(string username, string password)
        {
            

            if (UsernameTxtBox.Text.Equals("Username"))
            {
                UsernameTxtBox.Focus();
                return false;
            }
            else if (UsernameTxtBox.Text.Equals("Password"))
            {
                PasswordTxtBox.Focus();
                return false;
            }

            model.Login(username, password);

            if (model.LoginCheck() == true && !model.IsManager)
            {
                
                return true;
            }
            else
            {
                model.Logout();
                return false;
            }
                
            
        }
        //Log In Menu END

        #endregion

        #region CreateAccount
        //Create Account Menu BEGIN
        private void BackBtn2_Click(Object sender, EventArgs e)
        {
            SwitchMenu(LoginPanel);
        }

        private void CreateUsername_Enter(Object sender, EventArgs e)
        {
            if (CreateUsername.Text == "Username")
            {
                CreateUsername.Text = string.Empty;
            }
        }

        private void CreateUsername_Leave(Object sender, EventArgs e)
        {
            if (CreateUsername.Text == string.Empty)
            {
                CreateUsername.Text = "Username";
            }
        }

        private void CreatePassword_Enter(Object sender, EventArgs e)
        {
            if (CreatePassword.Text == "Password")
            {
                CreatePassword.Text = string.Empty;
                if (!ViewPasswordCheck2.Checked)
                {
                    CreatePassword.PasswordChar = '*';
                }
            }
        }

        private void CreatePassword_Leave(Object sender, EventArgs e)
        {
            if (CreatePassword.Text == string.Empty)
            {
                CreatePassword.PasswordChar = '\0';
                CreatePassword.Text = "Password";
            }
        }

        private void ViewPasswordCheck2_Changed(Object sender, EventArgs e)
        {
            if (ViewPasswordCheck2.Checked)
            {
                CreatePassword.PasswordChar = '\0';
            }
            else if (CreatePassword.PasswordChar == '\0' && CreatePassword.Text != "Password")
            {
                CreatePassword.PasswordChar = '*';
            }
        }

        private void CreateAccountPanel_Click(object sender, EventArgs e)
        {
            CreateAccountPanel.Focus();
        }

        private void CreateName_Enter(Object sender, EventArgs e)
        {
            if (CreateName.Text == "Name")
            {
                CreateName.Text = string.Empty;
            }
        }

        private void CreateName_Leave(Object sender, EventArgs e)
        {
            if (CreateName.Text == string.Empty)
            {
                CreateName.Text = "Name";
            }
        }

        private void CreateAddress1_Enter(Object sender, EventArgs e)
        {
            if (CreateAddress1.Text == "Address 1")
            {
                CreateAddress1.Text = string.Empty;
            }
        }

        private void CreateAddress1_Leave(Object sender, EventArgs e)
        {
            if (CreateAddress1.Text == string.Empty)
            {
                CreateAddress1.Text = "Address 1";
            }
        }

        private void CreateAddress2_Enter(Object sender, EventArgs e)
        {
            if (CreateAddress2.Text == "Address 2 / Delivery Instructions (Optional)")
            {
                CreateAddress2.Text = string.Empty;
            }
        }

        private void CreateAddress2_Leave(Object sender, EventArgs e)
        {
            if (CreateAddress2.Text == string.Empty)
            {
                CreateAddress2.Text = "Address 2 / Delivery Instructions (Optional)";
            }
        }

        private void CreateCity_Enter(Object sender, EventArgs e)
        {
            if (CreateCity.Text == "City")
            {
                CreateCity.Text = string.Empty;
            }
        }

        private void CreateCity_Leave(Object sender, EventArgs e)
        {
            if (CreateCity.Text == string.Empty)
            {
                CreateCity.Text = "City";
            }
        }

        private void CreateZIP_Enter(Object sender, EventArgs e)
        {
            if (CreateZIP.Text == "ZIP")
            {
                CreateZIP.Text = string.Empty;
            }
        }

        private void CreateZIP_Leave(Object sender, EventArgs e)
        {
            if (CreateZIP.Text == string.Empty)
            {
                CreateZIP.Text = "ZIP";
            }
        }

        private void CreateZIP_KeyPress(Object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void FinishedBtn_Click(object sender, EventArgs e)
        {
            if (checkFilled())
            {
                //Add data to database
                model.CreateAccount(CreateUsername.Text, CreatePassword.Text, CreateName.Text, CreateAddress1.Text,
                    CreateAddress2.Text, CreateCity.Text, SelectState.Text, CreateZIP.Text);


                SwitchMenu(OrderMenuPanel);
                model.StartNewOrder();
                //Random rnd = new Random();
                //Add check to make sure order number is not in database
                //orderNum = rnd.Next(100000, 1000000);
                //OrderNumLbl.Text = "Order #  " + orderNum;
            }
        }

        private bool checkFilled()
        {
            if (CreateUsername.Text == "Username")
            {
                CreateUsername.Focus();
                return false;
            }
            else if (CreatePassword.Text == "Password")
            {
                CreatePassword.Focus();
                return false;
            }
            else if (CreateName.Text == "Name")
            {
                CreateName.Focus();
                return false;
            }
            else if (CreateAddress1.Text == "Address 1")
            {
                CreateAddress1.Focus();
                return false;
            } else if (CreateCity.Text == "City")
            {
                CreateCity.Focus();
                return false;
            } else if (CreateZIP.Text == "ZIP")
            {
                CreateZIP.Focus();
                return false;
            } else if (SelectState.Text == "State")
            {
                SelectState.Focus();
                return false;
            }

            return true;
        }
        //Create Account Menu END
        #endregion

        #region OrderMenu
        //Order Menu BEGIN
        private void PizzaSizes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < PizzaSizes.Items.Count; i++)
            {
                if (i != e.Index) PizzaSizes.SetItemChecked(i, false);
            }
        }

        private void PizzaSizes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PizzaSizes.Items.Count; i++)
            {
                if (PizzaSizes.GetItemRectangle(i).Contains(PizzaSizes.PointToClient(MousePosition)))
                {
                    switch (PizzaSizes.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            PizzaSizes.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            PizzaSizes.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }
        }

        private void CrustType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < CrustType.Items.Count; i++)
            {
                if (i != e.Index) CrustType.SetItemChecked(i, false);
            }
        }

        private void CrustType_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CrustType.Items.Count; i++)
            {
                if (CrustType.GetItemRectangle(i).Contains(CrustType.PointToClient(MousePosition)))
                {
                    switch (CrustType.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            CrustType.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            CrustType.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }
        }

        private void Toppings_ItemCheck(Object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && Toppings.CheckedItems.Count >= 5)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }

        private void Toppings_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Toppings.Items.Count; i++)
            {
                if (Toppings.GetItemRectangle(i).Contains(Toppings.PointToClient(MousePosition)))
                {
                    switch (Toppings.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            Toppings.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            Toppings.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }
        }

        private void Drinks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < Drinks.Items.Count; i++)
            {
                if (i != e.Index) Drinks.SetItemChecked(i, false);
            }
        }

        private void Drinks_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Drinks.Items.Count; i++)
            {
                if (Drinks.GetItemRectangle(i).Contains(Drinks.PointToClient(MousePosition)))
                {
                    switch (Drinks.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            Drinks.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            Drinks.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }
        }

        private void DrinkSize_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < DrinkSize.Items.Count; i++)
            {
                if (i != e.Index) DrinkSize.SetItemChecked(i, false);
            }
        }

        private void DrinkSize_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DrinkSize.Items.Count; i++)
            {
                if (DrinkSize.GetItemRectangle(i).Contains(DrinkSize.PointToClient(MousePosition)))
                {
                    switch (DrinkSize.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            DrinkSize.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            DrinkSize.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }
        }

        private int orderIncr = 1;
        private string orderTxt = "";
        //Maybe total price
        private void AddToOrderBtn_Click(object sender, EventArgs e)
        {
            
            if (PizzaSizes.CheckedItems.Count == 0 || CrustType.CheckedItems.Count == 0 || Toppings.CheckedItems.Count == 0) return;

            if ((Drinks.CheckedItems.Count != 0 && DrinkSize.CheckedItems.Count == 0) || (Drinks.CheckedItems.Count == 0 && DrinkSize.CheckedItems.Count != 0)) return;

            //Sizes
            sizeEnme size = (sizeEnme)PizzaSizes.CheckedIndices[0]; //get the pizza size based on the buttons postion and converts it to a sizeEnme
            
            //Crust
            CrustEnme crust = (CrustEnme)CrustType.CheckedIndices[0];

            //Toppings
            List<TopingsEnme> toppings = new List<TopingsEnme>();

      
            foreach (int i in Toppings.CheckedIndices)
            {
                toppings.Add((TopingsEnme)i);
            }
           
            model.addItem(new Pizza(size, crust, toppings.ToArray()));
            
            //Drinks
            DrinkSizeEnum drinksize = (DrinkSizeEnum)DrinkSize.CheckedIndices[0];
            DrinkTypeEnum drinktype = (DrinkTypeEnum)Drinks.CheckedIndices[0];

            model.addItem(new Drink(drinktype, drinksize));


            orderTxt = model.ReceiptInfo().Replace("\n", Environment.NewLine);
            //--
        }

        private void CheckoutBtn_Click(object sender, EventArgs e)
        {
            if (CurrentOrderTxtBox.Text != string.Empty) SwitchMenu(PaymentPanel);
        }

        private void OrderHistoryBtn_Click(object sender, EventArgs e)
        {
            SwitchMenu(OrderHistoryPanel);
        }

        //Order Menu END
        #endregion

        #region PaymentProcessing
        //Payment Processing Menu BEGIN
        private void BackBtn3_Click(object sender, EventArgs e)
        {
            SwitchMenu(OrderMenuPanel);
        }
        
        private void PaymentPanel_Click(object sender, EventArgs e)
        {
            PaymentPanel.Focus();
        }

        private void DeliveryOrPickup_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < DeliveryOrPickup.Items.Count; i++)
            {
                if (i != e.Index) DeliveryOrPickup.SetItemChecked(i, false);
            }
        }

        private void DeliveryOrPickup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DeliveryOrPickup.Items.Count; i++)
            {
                if (DeliveryOrPickup.GetItemRectangle(i).Contains(DeliveryOrPickup.PointToClient(MousePosition)))
                {
                    switch (DeliveryOrPickup.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            DeliveryOrPickup.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            DeliveryOrPickup.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }
        }

        private void DoneBtn_Click(object sender, EventArgs e)
        {
            if (PaymentType.CheckedItems.Count == 0) return;

            if (PaymentType.CheckedItems[0].ToString() == "Card")
            {
                if (VisaOrMastercard.CheckedItems.Count == 0) return;
                if (CardNumTxtBox.Text == string.Empty)
                {
                    CardNumTxtBox.Focus();
                    return;
                }

                if (CardNameTxtBox.Text == string.Empty)
                {
                    CardNameTxtBox.Focus();
                    return;
                }

                if (ExpiryDateTxtBox.Text == string.Empty)
                {
                    ExpiryDateTxtBox.Focus();
                    return;
                }

                if (CVVTxtBox.Text == string.Empty)
                {
                    CVVTxtBox.Focus();
                    return;
                }
            }

            if (PaymentType.CheckedItems[0].ToString() == "Check")
            {
                if (AccountNumTxtBox.Text == string.Empty)
                {
                    AccountNumTxtBox.Focus();
                    return;
                }

                if (RouteNumTxtBox.Text == string.Empty)
                {
                    RouteNumTxtBox.Focus();
                    return;
                }
            }

            if (DeliveryOrPickup.CheckedItems.Count == 0) return;

            if (SignatureTxtBox.Text == string.Empty)
            {
                SignatureTxtBox.Focus();
                return;

            }

            //All Checks Done. Go to Receipt screen
        }

        private void PaymentType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < PaymentType.Items.Count; i++)
            {
                if (i != e.Index) PaymentType.SetItemChecked(i, false);
            }

            

        }

        private void PaymentType_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PaymentType.Items.Count; i++)
            {
                if (PaymentType.GetItemRectangle(i).Contains(PaymentType.PointToClient(MousePosition)))
                {
                    switch (PaymentType.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            PaymentType.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            PaymentType.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }

            string paymentType = PaymentType.CheckedItems[0].ToString();

            if (paymentType == "Card")
            {
                CardInfoPanel.Show();
                CheckInfoPanel.Hide();
                CashInfoPanel.Hide();
            }
            else if (paymentType == "Check")
            {
                CardInfoPanel.Hide();
                CheckInfoPanel.Show();
                CashInfoPanel.Hide();
            }
            else if (paymentType == "Cash")
            {
                CardInfoPanel.Hide();
                CheckInfoPanel.Hide();
                CashInfoPanel.Show();
            }
        }

        private void VisaOrMastercard_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < VisaOrMastercard.Items.Count; i++)
            {
                if (i != e.Index) VisaOrMastercard.SetItemChecked(i, false);
            }
        }

        private void VisaOrMastercard_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < VisaOrMastercard.Items.Count; i++)
            {
                if (VisaOrMastercard.GetItemRectangle(i).Contains(VisaOrMastercard.PointToClient(MousePosition)))
                {
                    switch (VisaOrMastercard.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            VisaOrMastercard.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            VisaOrMastercard.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }
                }
            }
        }

        private void CVVTxtBox_KeyPress(Object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void AccountNumTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void RouteNumTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        //Payment Processing Menu END

        #endregion

        #region ReceiptMenu
        //Receipt Menu BEGIN
        private void HomeBtn_Click(object sender, EventArgs e)
        {
            SwitchMenu(MainMenuPanel);
        }
        //Receipt Menu END
        #endregion

        #region ManagerDatabaseMenu
        //Manager Database BEGIN
        private void BackBtn5_Click(object sender, EventArgs e)
        {
            SwitchMenu(MainMenuPanel);
        }
        //Manager Database END
        #endregion

        #region OrderHistoryMenu
        //Order History Menu BEGIN
        private void BackBtn4_Click(object sender, EventArgs e)
        {
            if(model.IsManager)
            {
                SwitchMenu(MainMenuPanel);
            }
            else
            {
                SwitchMenu(OrderMenuPanel);
            }
        }

        private void PrintReceiptHistory(int id)
        {
            MessageBox.Show(model.GetCustomerOrders()[id]);
        }
        private void LoadOrderHistory()
        {
            string dateTime;
            foreach(string s in model.GetCustomerOrders())
            {
                dateTime = s.Split(":")[3];
                //place box made here
                //make button
            }
            //here is where we do things and stuff, but like good things not bad things. This things are importat and not big dum. 
            /*
             
           _MMMM_ 
            |OO|
            |- |
         >--[=]]--<
            |: |
            |__|
            [__]
             l l
             
              ______
             /      \
            |   /\   |
            |  /  \  |
            | |    | |
            | |    | |
            | |    | |
            | |    | |
            |  \  /  |
            |   \/   |
             \______/
              
            0W0     we all hawe Dwemans
            UwU     and swome Twimes
            OwO     dey win
             */
        }
        //Order History Menu END
        #endregion
    }
}
