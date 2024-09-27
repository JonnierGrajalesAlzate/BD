using PROCESO_CRUD.Logica;
using PROCESO_CRUD.Modelo;
using System;
using System.Windows.Forms;

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
            Persona oPersona = new Persona()
            { 
                pcNombrePersona = txtNombre.Text,
                pnSueldoPersona = double.Parse(txtSueldo.Text)
            };

            bool llRespuesta = PersonaLogica.Instacia.Guardar(oPersona);

            if (llRespuesta)
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
            dgvPersonas.AutoGenerateColumns = false; 
            dgvPersonas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "pnCedulaPersona",          
                HeaderText = "id",       
                DataPropertyName = "pnIdPersona" 
            });

            dgvPersonas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "pcNombrePersona",
                HeaderText = "Nombre",
                DataPropertyName = "pcNombrePersona"
            });

            dgvPersonas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "pnSueldoPersona",
                HeaderText = "Sueldo",
                DataPropertyName = "pnSueldoPersona"
            });

            mostrar_personas();
        }

         

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Persona oPersona = new Persona()
            {
                pnIdPersona = int.Parse(txtId.Text) 
            };

            bool llRespuesta = PersonaLogica.Instacia.Eliminar(oPersona);

            if (llRespuesta)
            {
                limpiar();
                mostrar_personas();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Persona oPersona = new Persona()
            {
                pnIdPersona = int.Parse(txtId.Text),
                pcNombrePersona = txtNombre.Text,
                pnSueldoPersona = double.Parse(txtSueldo.Text)
            };

            bool llRespuesta = PersonaLogica.Instacia.Actualizar(oPersona);

            if (llRespuesta)
            {
                limpiar();
                mostrar_personas();
            }
        }
    }
}
