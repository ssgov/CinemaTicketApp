using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CinemaTicketApp
{
    public class ReservationManager
    {
        private List<TicketReservation> reservations = new List<TicketReservation>();

        public void AddReservation(TicketReservation reservation)
        {
            reservations.Add(reservation);
            MessageBox.Show("Nowa rezerwacja została dodana:\n" + reservation.ToString(),
                "Rezerwacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public List<TicketReservation> GetReservations()
        {
            return reservations;
        }
    }
}
