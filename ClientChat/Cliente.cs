using ClientChat.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace ClientChat
{
    public partial class Cliente : Form, IService1Callback
    {
        Service1Client cliente;

        public Cliente()
        {
            InitializeComponent();
        }

        private void EnviarMensagem(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 3)
            {
                if(textBox3.Text.Length > 0){
                    cliente.PublishMsg(textBox1.Text + " ->" + textBox3.Text);
                }
            }
            else
            {
                MessageBox.Show("Digite um nome de usuario com 3 a 15 caracteres.", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            
        }

        public void sendMessage(string msg)
        {
            textBox2.AppendText(msg+Environment.NewLine);
        }

        private void loaded(object sender, EventArgs e)
        {
            
            cliente = new Service1Client(new InstanceContext(this));
            cliente.Subscribe();
        }

        private void closing(object sender, FormClosingEventArgs e)
        {
            cliente.Unsubscribe();
        }
    }
}
