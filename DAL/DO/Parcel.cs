﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public int DroneId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Associated { get; set; }
        public DateTime? Collected { get; set; }
        public DateTime? Delivered { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// the method override ToString method
        /// </summary>
        public override string ToString()
        {
            return $"ParcelId: {Id } SenderId: {SenderId} GetterId:" +
                $" {TargetId} Weight: {Weight} Status: {Priority} DroneId: {DroneId}" +
                $" Scheduled: {Associated}  Requested:{Created} " +
                $"PickedUp:{Collected} Delivered:{Delivered}";
        }
    }
}

