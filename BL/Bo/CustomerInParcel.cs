﻿using Bo;


namespace BO
{
    public class CustomerInParcel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => this.ToStringProps();
    }

}

