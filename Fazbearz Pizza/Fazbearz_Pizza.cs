using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Fazbearz_Pizza
{
    public partial class Fazbearz_Pizza : Form
    {
        private Panel[] panels;
        private Model model;

        public Fazbearz_Pizza()
        {
            InitializeComponent();
            model = new Model();
        }

        /// <summary>
        /// Loads all panels.
        /// Starts application at main menu.
        /// Sets tags of all input fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fazbearz_Pizza_Load(object sender, EventArgs e)
        {
            panels = Controls.OfType<Panel>().ToArray();
            SwitchMenu(MainMenuPanel);
            foreach (Control c in Controls) { SetTags(c); }
        }

        /// <summary>
        /// Switches screens by taking in Panel name panel
        /// </summary>
        /// <param name="panel"></param>
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
        
        /// <summary>
        /// Handles login button on the main menu that switches to login screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            SwitchMenu(LoginPanel);
        }

        /// <summary>
        /// Handles the exit button that closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_Click(Object sender, EventArgs e)
        {
            this.Close();
        }
 
        #endregion

        #region Login

        /// <summary>
        /// Handles the back button that returns from login to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_Click(Object sender, EventArgs e)
        {
            SwitchMenu(MainMenuPanel);
        }

        /// <summary>
        /// Handles the button that takes user to create an account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the login button once username and password information are passed in. Checks to see if the username and password are valid. 
        /// If they are valid, switches to order menu panel. If the username and password are for the manager, switches to the manager database panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActualLoginBtn_Click(Object sender, EventArgs e)
        {
            string username = UsernameTxtBox.Text;
            string password = PasswordTxtBox.Text;
            bool validCustomerLogin = LogIn(username, password);
            
            if (validCustomerLogin)
            {
                SwitchMenu(OrderMenuPanel);
                model.StartNewOrder();
                UsernameTxtBox.Text = "Username";
                PasswordTxtBox.Text = "Password";

            }
            else if(model.IsManager)
            {
                SwitchMenu(ManagerDatabasePanel);
                LoadFazeBase();
                UsernameTxtBox.Text = "Username";
                PasswordTxtBox.Text = "Password";
            }
               
            else
            {
                //bounce
            }

        }

        /// <summary>
        /// Returns a true boolean and sets currrentUser if username and password match.
        /// Otherwise returns false if username and password dont match or the user is a manager.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        #endregion

        #region CreateAccount

        /// <summary>
        /// Handles the back button that takes the user back to he login page.
        /// Resets all text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn2_Click(Object sender, EventArgs e)
        {
            SwitchMenu(LoginPanel);
            foreach (Control c in Controls) { ClearControls(c); }
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
        

        /// <summary>
        /// Button handler that executes when the user is finished creating an account.
        /// Checks if all information is filled. If it is, a new account is created in the model with all passed in information. 
        /// The panel is switched to the order menu panel and a new order is started in the model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishedBtn_Click(object sender, EventArgs e)
        {
            if (checkFilled())
            {
                //Add data to database
                foreach(dataBaseObject obj in model.GetDataBaseArray())
                {
                    if (obj.customer.username.Equals(CreateUsername.Text.Trim()))
                    {
                        CreateUsername.Text = string.Empty;
                        CreateUsername.Focus();
                        return;
                    }

                }
                
                model.CreateAccount(CreateUsername.Text.Trim(), CreatePassword.Text.Trim(), CreateName.Text.Trim(), CreateAddress1.Text.Trim(),
                    CreateAddress2.Text.Trim(), CreateCity.Text.Trim(), SelectState.Text.Trim(), CreateZIP.Text.Trim());


                SwitchMenu(OrderMenuPanel);
                model.StartNewOrder();
   
            }
        }

        /// <summary>
        /// Returns true if all text fields are filled.
        /// Else returns false.
        /// </summary>
        /// <returns></returns>
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

        #endregion

        #region OrderMenu

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

        private string orderTxt = "";
 

        /// <summary>
        /// Adds nothing to order if no boxes are checked. Converts the checked boxes of sizes, crust, toppings, drinks, and drink sizes to enums. 
        /// Either a pizza or a drink must be fully customized, or both a pizza and a drink must be fully customized to add to currentOrder in model.
        /// Order info is passed to the orderTxt string and displayed on screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToOrderBtn_Click(object sender, EventArgs e)
        {
            if (PizzaSizes.CheckedItems.Count == 0 || CrustType.CheckedItems.Count == 0 ) { }
            else
            {
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
                orderTxt = model.OrderInfo().Replace("\n", Environment.NewLine);
                CurrentOrderTxtBox.Text = orderTxt;
            }

            if ((DrinkSize.CheckedItems.Count == 0) || (Drinks.CheckedItems.Count == 0)) return;
            else
            {
                //Drinks
                DrinkSizeEnum drinksize = (DrinkSizeEnum)DrinkSize.CheckedIndices[0];
                DrinkTypeEnum drinktype = (DrinkTypeEnum)Drinks.CheckedIndices[0];

                model.addItem(new Drink(drinktype, drinksize));
                orderTxt = model.OrderInfo().Replace("\n", Environment.NewLine);
                CurrentOrderTxtBox.Text = orderTxt;
            }
            
            
   
        }

        /// <summary>
        /// Handels the checkout button which switches Panel to payment panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckoutBtn_Click(object sender, EventArgs e)
        {
            if (CurrentOrderTxtBox.Text != string.Empty) SwitchMenu(PaymentPanel);
        }

        /// <summary>
        /// Handles the order history button which switches Panel to order history panel and LoadsOrderHistory().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderHistoryBtn_Click(object sender, EventArgs e)
        {
            SwitchMenu(OrderHistoryPanel);
            LoadOrderHistory();
            
        }

        /// <summary>
        /// Handles the back button which switches order menu Panel to login panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn6_Click(object sender, EventArgs e)
        {
            SwitchMenu(LoginPanel);
            foreach (Control c in Controls) { ClearControls(c); }

        }

        #endregion

        #region PaymentProcessing

        /// <summary>
        /// Handles the back button which switches Panel back to order menu panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (i != e.Index)
                {
                    // if (i != e.Index) PaymentType.SetItemChecked(i, false);
                    DeliveryOrPickup.SetItemChecked(i, false);
                    model.currentOrder.isPickUp = (1 != i);
                }
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

        /// <summary>
        /// Handles the done with payment button which returns if nothing is entered.
        /// Sets currentOrder payment type based on the passed in enum.
        /// Switches Panel to receipt Panel.
        /// Adds ReceiptInfo() to receipt text box.
        /// Adds the order to the current customer in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            model.currentOrder.paymentType = (PaymentTypeEnum)PaymentType.CheckedIndices[0];
            SwitchMenu(ReceiptPanel);
            ReceiptTxtBox.Text = model.ReceiptInfo(SignatureTxtBox.Text).Replace("\n", Environment.NewLine);
            model.AddOrderToCustomer();


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
            string paymentType = "";
            if (PaymentType.CheckedItems.Count != 0) paymentType = PaymentType.CheckedItems[0].ToString();

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

        #endregion

        #region ReceiptMenu

        /// <summary>
        /// Handles the home button which switches Panel to main menu panel.
        /// Resets all input fields in the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeBtn_Click(object sender, EventArgs e)
        {
            SwitchMenu(MainMenuPanel);

            foreach(Control c in Controls) { ClearControls(c); }


        }

        private void PrintReceiptBtn(Object sender, EventArgs e)
        {
            model.PrintReceiptToPDF();
        }

        /// <summary>
        /// Clears all Controls in the form.
        /// </summary>
        /// <param name="control"></param>
        private void ClearControls(Control control)
        {

            foreach (var c in control.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)c).Text = String.Empty;
                    ((System.Windows.Forms.TextBox)c).Text = ((System.Windows.Forms.TextBox)c).Tag.ToString();
                    ClearControls((System.Windows.Forms.TextBox)c);
                }

                if(c is CheckedListBox)
                {
                    for (int i = 0; i < ((CheckedListBox)c).Items.Count; i++)
                    {
                        //if (i != ((CheckedListBox)c).Index) 
                            ((CheckedListBox)c).SetItemChecked(i, false);
                    }
                    
                    ClearControls((CheckedListBox)c);
                }
                
            }
        }

        /// <summary>
        /// Makes the tag of every Control equal its current text
        /// </summary>
        /// <param name="control"></param>
        private void SetTags(Control control)
        {

            foreach (var c in control.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)c).Tag = ((System.Windows.Forms.TextBox)c).Text;
                    SetTags((System.Windows.Forms.TextBox)c);
                }
                
            }
        }
        #endregion

        #region ManagerDatabaseMenu

        /// <summary>
        /// Handles the back button which switches panel from manager database Panel to main menu panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn5_Click(object sender, EventArgs e)
        {
            SwitchMenu(MainMenuPanel);
        }

        /// <summary>
        /// Initializes all buttons to view orders on the manager database panel.
        /// </summary>
        /// <param name="i"></param>
        private void CreateManagerButtons(int i)
        {
            System.Windows.Forms.Button newButton = new System.Windows.Forms.Button();
            newButton.Text = "View Order History";
            newButton.Size = new Size(150, 40);
            newButton.Click += delegate (object? sender, EventArgs e) { 
            
                model.SetCurrentUser(model.GetDataBaseArray()[i]);

                LoadOrderHistory();
                SwitchMenu(OrderHistoryPanel);
            };
            ManagerDataTable.Controls.Add(newButton, 4, ManagerDataTable.RowCount - 1);
        }

        /// <summary>
        /// Initializes the manager database.
        /// Calls CreateManagerButtons().
        /// </summary>
        private void LoadFazeBase()
        {
            while (ManagerDataTable.Controls.Count > 0)
            {
                ManagerDataTable.Controls[0].Dispose();
            }

            
            int index = 0;
            foreach (dataBaseObject s in model.GetDataBaseArray())
            {

                ManagerDataTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                
                ManagerDataTable.Controls.Add(new Label() { Text = s.customer.name , Dock = DockStyle.Fill }, 0, ManagerDataTable.RowCount - 1);

                ManagerDataTable.Controls.Add(new Label() { Text = s.customer.username, Dock = DockStyle.Fill }, 1, ManagerDataTable.RowCount - 1);

                ManagerDataTable.Controls.Add(new Label() { Text = s.customer.address + " "+ s.customer.city+" " + s.customer.state + " " + s.customer.zipcode, Dock = DockStyle.Fill }, 2, ManagerDataTable.RowCount - 1);

                ManagerDataTable.Controls.Add(new Label() { Text = s.customer.directions, Dock = DockStyle.Fill }, 3, ManagerDataTable.RowCount - 1);

                CreateManagerButtons(index);
                ManagerDataTable.RowCount++;
                index++;
            }

            
            if(ManagerDataTable.RowCount > 0)
                ManagerDataTable.RowCount--;

        }

        #endregion

        #region OrderHistoryMenu
 
        /// <summary>
        /// Handles the back button on the order history panel.
        /// If the user is the manager, Panel switches to manager database panel.
        /// If the user is a customer, Panel switches to the order menu panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn4_Click(object sender, EventArgs e)
        {
            if(model.IsManager)
            {
                SwitchMenu(ManagerDatabasePanel);
            }
            else
            {
                SwitchMenu(OrderMenuPanel);
                
            }
        }


        /// <summary>
        /// Initialized all buttons to view receipts on the order history panel.
        /// </summary>
        /// <param name="i"></param>
        private void CreateOrderButtons(int i)
        {
            System.Windows.Forms.Button newButton = new System.Windows.Forms.Button();
            newButton.Text = "View Receipt";
            newButton.Size = new Size(150, 40);
            newButton.Tag = i;
            newButton.Click += delegate (object? sender, EventArgs e) { MessageBox.Show(model.GetCustomerOrders()[i]); };
            OrderHistoryDataTable.Controls.Add(newButton,1, OrderHistoryDataTable.RowCount - 1);
        }

        /// <summary>
        /// Initializes the order history panel, establishing the date/time column and the button column.
        /// </summary>
        private void LoadOrderHistory()
        {
            while (OrderHistoryDataTable.Controls.Count > 0)
            {
                OrderHistoryDataTable.Controls[0].Dispose();
            }
           
            int index = 0;
            foreach(string s in model.GetCustomerOrders())
            {
                string dateTime = s.Split(":")[1]+":"+s.Split(":")[2] + ":" + s.Split(":")[3] ;
                OrderHistoryDataTable.RowStyles.Add(new RowStyle(SizeType.AutoSize)); //could add height
                OrderHistoryDataTable.Controls.Add(new Label() { Text = dateTime , Dock = DockStyle.Fill},0,OrderHistoryDataTable.RowCount - 1);

                CreateOrderButtons(index);
                OrderHistoryDataTable.RowCount++;
                index++;

            }
            if (model.GetCustomerOrders() != null && OrderHistoryDataTable.RowCount > 0) 
                OrderHistoryDataTable.RowCount--;
        }

        #endregion

        
    }
}
