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

        private string customerName;
        private string customerAddress;

        public Fazbearz_Pizza()
        {
            InitializeComponent();
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
            bool validLogin = LogIn(username, password);
            if (validLogin)
            {
                SwitchMenu(OrderMenuPanel);

                Random rnd = new Random();
                //Add check to make sure order number is not in database
                orderNum = rnd.Next(100000, 1000000);
                OrderNumLbl.Text = "Order #  " + orderNum;
            }
        }

        private bool LogIn(string username, string password)
        {
            if (UsernameTxtBox.Text == "Username")
            {
                UsernameTxtBox.Focus();
                return false;
            } else if (PasswordTxtBox.Text == "Password")
            {
                PasswordTxtBox.Focus();
                return false;
            }

            //Database check before log in
            return true;
        }
        //Log In Menu END

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
            if (CreateAddress2.Text == "Address 2 (Optional)")
            {
                CreateAddress2.Text = string.Empty;
            }
        }

        private void CreateAddress2_Leave(Object sender, EventArgs e)
        {
            if (CreateAddress2.Text == string.Empty)
            {
                CreateAddress2.Text = "Address 2 (Optional)";
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
                SwitchMenu(OrderMenuPanel);

                Random rnd = new Random();
                //Add check to make sure order number is not in database
                orderNum = rnd.Next(100000, 1000000);
                OrderNumLbl.Text = "Order #  " + orderNum;
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

        private void OrderMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        //Create Account Menu END

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
            float totalPrice = 0;

            if (PizzaSizes.CheckedItems.Count == 0 || CrustType.CheckedItems.Count == 0 || Toppings.CheckedItems.Count == 0) return;

            if ((Drinks.CheckedItems.Count != 0 && DrinkSize.CheckedItems.Count == 0) || (Drinks.CheckedItems.Count == 0 && DrinkSize.CheckedItems.Count != 0)) return;


            orderTxt += "Order " + orderIncr + ": " + Environment.NewLine + Environment.NewLine;

            orderTxt += "Size: ";
            for (int i = 0; i < PizzaSizes.Items.Count; i++)
            {
                if (PizzaSizes.GetItemChecked(i))
                {
                    orderTxt += PizzaSizes.Items[i].ToString();
                    string price = PizzaSizes.Items[i].ToString().Substring(PizzaSizes.Items[i].ToString().LastIndexOf('$') + 1);

                    totalPrice += float.Parse(price, CultureInfo.InvariantCulture.NumberFormat);
                }
            }
            orderTxt += Environment.NewLine;

            orderTxt += "Crust: ";
            for (int i = 0; i < CrustType.Items.Count; i++)
            {
                if (CrustType.GetItemChecked(i))
                {
                    orderTxt += CrustType.Items[i].ToString();
                    
                    if (CrustType.Items[i].ToString().Contains('$'))
                    {
                        string price = CrustType.Items[i].ToString().Substring(CrustType.Items[i].ToString().LastIndexOf('$') + 1);
                        
                        totalPrice += float.Parse(price, CultureInfo.InvariantCulture.NumberFormat);
                    }
                }
            }
            orderTxt += Environment.NewLine;

            orderTxt += "Toppings: ";
            for (int i = 0; i < Toppings.Items.Count; i++)
            {
                if (Toppings.GetItemChecked(i))
                {
                    orderTxt += Toppings.Items[i].ToString();

                    if (Toppings.CheckedItems[Toppings.CheckedItems.Count - 1].ToString() != Toppings.Items[i].ToString()) orderTxt += ", ";

                    totalPrice += 0.99f;
                }
            }
            orderTxt += Environment.NewLine;

            orderTxt += "Drink: ";

            for (int i = 0; i < Drinks.Items.Count; i++)
            {
                if (Drinks.GetItemChecked(i))
                {
                    orderTxt += Drinks.Items[i].ToString() + " ";
                }
            }

            for (int i = 0; i < DrinkSize.Items.Count; i++)
            {
                if (DrinkSize.GetItemChecked(i))
                {
                    orderTxt += DrinkSize.Items[i].ToString() + " ";
                    string price = DrinkSize.Items[i].ToString().Substring(DrinkSize.Items[i].ToString().LastIndexOf('$') + 1);

                    totalPrice += float.Parse(price, CultureInfo.InvariantCulture.NumberFormat);
                }
            }
            orderTxt += Environment.NewLine;

            float tax = totalPrice * 0.06f;
            orderTxt += "Tax: " + tax.ToString("c2") + Environment.NewLine;

            totalPrice += tax;

            orderTxt += "Total: " + totalPrice.ToString("c2") + Environment.NewLine + Environment.NewLine;

            orderIncr++;

            CurrentOrderTxtBox.Text = orderTxt;
        }

        private void CheckoutBtn_Click(object sender, EventArgs e)
        {
            if (CurrentOrderTxtBox.Text != string.Empty) SwitchMenu(PaymentPanel);
        }

        private void OrderHistoryBtn_Click(object sender, EventArgs e)
        {
            // Go to order history page
        }

        //Order Menu END

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
    }
}
