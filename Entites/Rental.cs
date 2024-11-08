﻿namespace AutoKolcsonzoAPIMSz.Entites
{
    public class Rental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        //navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Vehicle Vehicle { get; set; }

    }
}
