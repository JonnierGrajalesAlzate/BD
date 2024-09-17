using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROCESO_CRUD.Modelo;
using PROCESO_CRUD.Logica;

/*Jonnier Grajales Alzate
 16/09/2024
BaseDeDatos CRUD con Sqlite
 */

namespace PROCESO_CRUD
{
    public partial class fmr_Panel : Form
    {
        public fmr_Panel()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Persona objeto = new Persona()
            { 
                pcNombrePersona = txtNombre.Text,
                pnSueldoPersona = double.Parse(txtSueldo.Text)
            };

            bool respuesta = PersonaLogica.Instacia.Guardar(objeto);

            if (respuesta)
            {
                limpiar();
                mostrar_personas();
            }

        }

        public void mostrar_personas() {
            dgvPersonas.DataSource = null;
            dgvPersonas.DataSource = PersonaLogica.Instacia.Listar();
        }

        public void limpiar()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtSueldo.Text = "";
            txtNombre.Focus();
        }

        private void fmr_Panel_Load(object sender, EventArgs e)
        {
            mostrar_personas();
        }

         

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Persona objeto = new Persona()
            {
                pnIdPersona = int.Parse(txtId.Text) 
            };

            bool respuesta = PersonaLogica.Instacia.Eliminar(objeto);

            if (respuesta)
            {
                limpiar();
                mostrar_personas();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Persona objeto = new Persona()
            {
                pnIdPersona = int.Parse(txtId.Text),
                pcNombrePersona = txtNombre.Text,
                pnSueldoPersona = double.Parse(txtSueldo.Text)
            };

            bool respuesta = PersonaLogica.Instacia.Actualizar(objeto);

            if (respuesta)
            {
                limpiar();
                mostrar_personas();
            }
        }
    }
}
