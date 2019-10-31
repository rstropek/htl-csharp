using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// NOTE data annotations for class members

namespace CashDesk
{
    public class Member : IMember
    {
        [Key]
        public int MemberNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public List<Membership> Memberships { get; set; }
    }

    public class Membership : IMembership
    {
        public int MembershipID { get; set; }

        [Required]
        public Member Member { get; set; }

        private DateTime begin = DateTime.MinValue;

        [Required]
        public DateTime Begin
        {
            get { return begin; }
            set
            {
                if (value > End)
                {
                    throw new ArgumentException("Begin must be <= end", nameof(Begin));
                }

                begin = value;
            }
        }

        private DateTime end = DateTime.MaxValue;

        [Required]
        public DateTime End
        {
            get { return end; }
            set
            {
                if (Begin > value)
                {
                    throw new ArgumentException("Begin must be <= end", nameof(End));
                }

                end = value;
            }
        }

        IMember IMembership.Member => Member;

        public List<Deposit> Deposits { get; set; }
    }

    public class Deposit : IDeposit
    {
        public int DepositID { get; set; }

        [Required]
        public Membership Membership { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public decimal Amount { get; set; }

        IMembership IDeposit.Membership => Membership;
    }

    public class DepositStatistics : IDepositStatistics
    {
        public IMember Member { get; set; }

        public int Year { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
