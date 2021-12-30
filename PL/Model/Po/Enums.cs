﻿using System;

namespace PL.Model
{
    public class Enums
    {
        public enum WeightCategories { LIGHT = 1, INTERMEDIATE, HEAVY }

        public enum DroneStatuses { AVAILABLE, MAINTENANCE, TRANSPORT }

        public enum Priorities { REGULAR = 1, FAST, EMERGENCY }

        public enum Menu { ADD, UPDATE, DISPLAY, VIEW_LIST, EXIT }

        public enum Add { BASE_STATION, DRONE, CUSTOMER, PARCEL }

        public enum Update { DRONE, BASE_STATION, CUSTOMER, SCHEDULED, PICKED_UP, SUPPLY, SENDFORCHARGING, RELEASE }

        public enum Display { BASE_STATION, DRONE, CUSTOMER, PARCEL }

        public enum ViewList { BASE_STATIONS, DRONES, CUSTOMERS, PARCELS, PENDING_PARCELS, AVAILABLE_CHARGESLOTS }

        public enum DeliveryStatus { CREATED, BELONGED, COLLECTED, PROVIDED }
    }
}

