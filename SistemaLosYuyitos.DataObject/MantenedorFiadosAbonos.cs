using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SistemaLosYuyitos.Entidad;

namespace SistemaLosYuyitos.Controlador
{
    public class MantenedorFiadosAbonos
    {
        DataAccess.DataAccess da;
        public bool Create(Fiado fiado)
        {
            bool resCreate = false;
            bool resFiado = false;
            using (da = new DataAccess.DataAccess())
            {
                DataAccess.DataAccess da2;
                decimal total_abono = fiado.AbonosRealizados.Sum(x => x.MontoAbono);
                da.GenerarComando(@"insert into fiado (seq_id_fiado.nextval, boleta_nro_boleta, cliente_rut_cliente, fecha_fiado, fecha_vencimiento, total_abonos, total_pago, esta_vencido)
                                    values (:id_fiado, :nro_boleta, :rut_cliente, :fecha_fiado, :fecha_vencimiento, :total_abono, :total_pago, :vencido)
                                    returning id_fiado into :nuevo_id");
                da.AgregarParametro(":id_fiado", fiado.IdFiado, DbType.Int32);
                da.AgregarParametro(":nro_boleta", fiado.Boleta.NroBoleta, DbType.Int32);
                da.AgregarParametro(":rut_cliente", fiado.ClienteFiado.RutCliente);
                da.AgregarParametro(":fecha_fiado", fiado.FechaFiado, DbType.Date);
                da.AgregarParametro(":fecha_vencimiento", fiado.FechaVencimiento, DbType.Date);
                da.AgregarParametro(":total_abono", total_abono, DbType.Int32);
                da.AgregarParametro(":total_pago", fiado.TotalPago, DbType.Int32);
                da.AgregarParametro(":vencido", fiado.Vencido ? "y" : "n");
                da.AgregarParametroOut(":nuevo_id", DbType.Decimal);
                da.ExecuteNonQuery();
                decimal nuevo_id = Convert.ToDecimal(da.GetValorParametro(":nuevo_id"));
                if (resFiado)
                {

                }

            }
            return resCreate;
        }

        public Fiado Read(decimal id_fiado)
        {
            throw new NotImplementedException();
        }
        public bool Abonar(Abono abono, decimal id_fiado)
        {
            throw new NotImplementedException();
        }
        public List<Fiado> ListarFiados()
        {
            throw new NotImplementedException();
        }
        public List<Fiado> ListarFiadosPorCliente(string rut)
        {
            throw new NotImplementedException();
        }
        public List<Abono> ListarAbonos(decimal id_fiado)
        {
            throw new NotImplementedException();
        }
        public List<Abono> ListarAbonosPorCliente(string rut)
        {
            throw new NotImplementedException();
        }
    }
}
