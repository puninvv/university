using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    private Panel CreateVariableValuePair(char _var, double _val)
    {
        Panel result = new Panel();

        var variable = CreateVariable(_var);
        var value = CreateValue(_val);

        value.ID = "var_" + _var.ToString();

        result.Controls.Add(variable);
        result.Controls.Add(value);

        return result;
    }

    private Label CreateVariable(char _var)
    {
        Label result = new Label();

        result.Text = _var.ToString();

        return result;
    }

    private TextBox CreateValue(double _val)
    {
        TextBox result = new TextBox();

        result.Text = _val.ToString();
        result.Width = 40;

        return result;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        mVariables.Controls.Add(CreateVariableValuePair('a', 0));
        for (char ch = 'b'; ch <= 'z'; ch++)
        {
            Panel phToAdd = CreateVariableValuePair(ch, 0);
            mVariables.Controls.Add(phToAdd);
        }
    }
}