using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Models
{
    public class SettlementDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public SettlementType SettlementType { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public DateTime RequestDate { get; set; }
        public int Installments { get; set; }
        public decimal Downpayment { get; set; }
        public decimal MonthlyFee { get { return decimal.Round(SubTotal / Installments, 2, MidpointRounding.AwayFromZero); } }
        public decimal SubTotal { get { return Bills.Sum(b => b.Amount) - Downpayment; } } //Total mιnus DownPayment
        
        public SettlementDto()
        {
            Bills = new HashSet<Bill>();
        }

        public SettlementDto(Settlement s)
        {
            Id = s.Id;
            Title = s.Title;
            SettlementType = s.SettlementType;
            Bills = s.Bills;
            Installments = s.Installments;
            Downpayment = s.Downpayment;
            RequestDate = s.RequestDate;
        }

        public Settlement ToDomainModel(SettlementDto dto)
        {
            var settlement = new Settlement
            {
                Id = dto.Id,
                Title = dto.Title,
                Bills = dto.Bills,
                Downpayment = dto.Downpayment,
                Installments = dto.Installments,
                RequestDate = dto.RequestDate,
                SettlementType = dto.SettlementType,
                SettlementTypeId = dto.SettlementType.Id
            };
            return settlement;
        }


    }
}
