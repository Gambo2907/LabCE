namespace API.Messages
    /*
     * Clase donde su metodo MensajeReserva se encarga de realizar un string que modela el mensaje
     * que se enviara al correo del usuario que realice la reserva de un laboratorio. 
     */
{
    public class Message
    {
        public string MensajeReserva(string nombre,string laboratorio,string fecha, string hora_inicio, string hora_final)
        {
            string mensaje = "Hola " + nombre + " le informamos que su reservación ha sido confirmada correctamente. La información se encuentra a continuación.\n"
                + "Nombre del laboratorio: " + laboratorio + "\n"
                + "Fecha: " + fecha + "\n"
                + "Hora de inicio: " + hora_inicio + "\n"
                + "Hora de finalización: " + hora_final + "\n";
            return mensaje;
        }
    }
}
