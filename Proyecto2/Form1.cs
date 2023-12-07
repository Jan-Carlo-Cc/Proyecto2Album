using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2
{
    public partial class Form1 : Form
    {
        LinkedList<Foto> listaSimple = new LinkedList<Foto>();
        LinkedList<Foto> listaCircular = new LinkedList<Foto>();
        LinkedList<Foto> listaDoble = new LinkedList<Foto>();
        LinkedListNode<Foto> nodoActual;
        LinkedListNode<Foto> nodoActualDoble;
        LinkedListNode<Foto> nodoActualCircular;
        int id = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {

            BorrarFotoActuaSimple();

        }

        private void BorrarFotoActualCircular()
        {
            if (nodoActualCircular != null)
            {
                // Obtener el nodo anterior al actual
                LinkedListNode<Foto> nodoAnterior = listaCircular.Find(nodoActualCircular.Value).Previous;

                // Borrar el nodo actual de la lista
                listaCircular.Remove(nodoActualCircular);

                // Si la lista ahora está vacía, reiniciar el nodo actual a null
                if (listaCircular.First == null)
                {
                    nodoActualCircular = null;
                    pictureBox3.Image = null;
                }
                else
                {
                    // Si no estamos borrando el último nodo, avanzar al siguiente nodo
                    if (nodoAnterior != null)
                    {
                        nodoActualCircular = nodoAnterior.Next;
                    }
                    else
                    {
                        // Si estamos borrando el primer nodo, avanzar al nuevo primer nodo
                        nodoActualCircular = listaCircular.First;
                    }

                    // Mostrar la foto actual después de borrar
                    MostrarFotoActual();
                }
            }
            else
            {
                MessageBox.Show("No hay fotos para borrar.", "Lista vacía");
            }
        }

        private void BorrarFotoActuaSimple()
        {
            if (nodoActual != null)
            {
                // Obtener el nodo anterior al actual
                LinkedListNode<Foto> nodoAnterior = listaSimple.Find(nodoActual.Value).Previous;

                // Borrar el nodo actual de la lista
                listaSimple.Remove(nodoActual);

                // Si la lista ahora está vacía, reiniciar el nodo actual a null
                if (listaSimple.First == null)
                {
                    nodoActual = null;
                    pictureBox1.Image = null;
                }
                else
                {
                    // Si no estamos borrando el último nodo, avanzar al siguiente nodo
                    if (nodoAnterior != null)
                    {
                        nodoActual = nodoAnterior.Next;
                    }
                    else
                    {
                        // Si estamos borrando el primer nodo, avanzar al nuevo primer nodo
                        nodoActual = listaSimple.First;
                    }

                    // Mostrar la foto actual después de borrar
                    MostrarFotoActual();
                }
            }
            else
            {
                MessageBox.Show("No hay fotos para borrar.", "Lista vacía");
            }
        }


        private void BorrarFotoActuaDoble()
        {
            if (nodoActual != null)
            {
                // Obtener el nodo anterior al actual
                LinkedListNode<Foto> nodoAnterior = nodoActualDoble.Previous;

                // Borrar el nodo actual de la lista
                listaDoble.Remove(nodoActualDoble);

                // Si la lista ahora está vacía, reiniciar el nodo actual a null
                if (listaDoble.First == null)
                {
                    nodoActualDoble = null;
                    pictureBox2.Image = null;
                }
                else
                {
                    // Si no estamos borrando el último nodo, avanzar al siguiente nodo
                    if (nodoAnterior != null)
                    {
                        nodoActualDoble = nodoAnterior.Next;

                        // Si estamos borrando el último nodo, volver al primer nodo
                        if (nodoActualDoble == null)
                        {
                            nodoActualDoble = listaDoble.First;
                        }
                    }
                    else
                    {
                        // Si estamos borrando el primer nodo, avanzar al nuevo primer nodo
                        nodoActualDoble = listaDoble.First;
                    }

                    // Mostrar la foto actual después de borrar
                    MostrarFotoActual();
                }
            }
            else
            {
                MessageBox.Show("No hay fotos para borrar.", "Lista vacía");
            }
        }



        //SELECCIONAR ARCHIVO


        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Archivos de imágenes|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = openFileDialog.FileName;

                txtRuta.Text = rutaArchivo;

                string nombreArchivo = Path.GetFileName(rutaArchivo);
                txtDescripcion.Text = nombreArchivo;
                id++;
                txtID.Text = id.ToString();
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRuta.Text))
            {
                MessageBox.Show("Por favor ingrese una imagen para guardar");
            }
            else
            {
                string ruta = txtRuta.Text;
                string descrip = txtDescripcion.Text;
                int id = int.Parse(txtID.Text);
                listaSimple.AddLast(new Foto(id, descrip, ruta));
                listaDoble.AddLast(new Foto(id, descrip, ruta));
                listaCircular.AddLast(new Foto(id, descrip, ruta));

                txtRuta.Clear();
                txtDescripcion.Clear();
                txtID.Clear();
                MostrarPrimeraFoto();
                MostrarPrimeraFotoDoble();
                MostrarPrimeraFotoCircular();
            }

        }


        // LISTA SIMPLEMENTE ENLAZADA




        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (nodoActual != null && nodoActual.Next != null)
            {
                // Avanzar al siguiente nodo
                nodoActual = nodoActual.Next;

                MostrarFotoActual();
            }
            else
            {
                MessageBox.Show("No hay más fotos disponibles.", "Fin de la lista");
            }
        }

        private void MostrarPrimeraFoto()
        {
            if (listaSimple.First != null)
            {
                nodoActual = listaSimple.First;

                MostrarFotoActual();
            }
            else
            {
                MessageBox.Show("No hay fotos disponibles.", "Lista vacía");
            }
        }

        private void MostrarFotoActual()
        {
            Foto fotoActual = nodoActual.Value;

            pictureBox1.ImageLocation = fotoActual.Ruta;

        }


        //LISTA DOBLEMENTE ENLAZADA CODIGO


        private void btnSiguienteDoble_Click(object sender, EventArgs e)
        {
            if (nodoActualDoble != null && nodoActualDoble.Next != null)
            {
                // Avanzar al siguiente nodo
                nodoActualDoble = nodoActualDoble.Next;

                MostrarFotoActualDoble();
            }
            else
            {
                MessageBox.Show("No hay más fotos disponibles.", "Fin de la lista");
            }
        }
        private void MostrarPrimeraFotoDoble()
        {
            if (listaDoble.First != null)
            {
                nodoActualDoble = listaDoble.First;

                MostrarFotoActualDoble();
            }
            else
            {
                MessageBox.Show("No hay fotos disponibles.", "Lista vacía");
            }
        }

        private void MostrarFotoActualDoble()
        {
            Foto fotoActual = nodoActualDoble.Value;

            pictureBox2.ImageLocation = fotoActual.Ruta;

        }

        private void btnAnterior_Click_1(object sender, EventArgs e)
        {
            if (nodoActualDoble != null && nodoActualDoble.Previous != null)
            {
                nodoActualDoble = nodoActualDoble.Previous;

                MostrarFotoActualDoble();
            }
            else
            {
                MessageBox.Show("No hay fotos anteriores.", "Inicio de la lista");
            }
        }


        //LISTA CIRCULAR CODIGO



        private void ConvertirEnListaEnlazadaCircular(LinkedList<Foto> lista)
        {
            if (lista.First.Previous != null)
            {
                lista.AddLast(lista.First.Value);
                //MostrarPrimeraFotoCircular();
            }

            //MostrarFotoActualCircular();

        }


        private void btnSiguienteCircular_Click(object sender, EventArgs e)
        {


            if (nodoActualCircular != null)
            {
                nodoActualCircular = nodoActualCircular.Next;

                MostrarFotoActualCircular();
                if (nodoActualCircular == listaCircular.Last)
                {
                    MostrarPrimeraFotoCircular();
                    nodoActualCircular = listaCircular.First;
                    ConvertirEnListaEnlazadaCircular(listaCircular);
                }

            }
            else
            {
                MessageBox.Show("No hay fotos disponibles.", "Lista vacía");
            }
        }



        private void MostrarPrimeraFotoCircular()
        {
            if (listaCircular.First != null)
            {
                nodoActualCircular = listaCircular.First;

                MostrarFotoActualCircular();
            }
            else
            {
                MessageBox.Show("No hay fotos disponibles.", "Lista vacía");
            }
        }



        private void MostrarFotoActualCircular()
        {
            Foto fotoActual = nodoActualCircular.Value;

            pictureBox3.ImageLocation = fotoActual.Ruta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BorrarFotoActuaDoble();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BorrarFotoActualCircular();
        }
    }
}
