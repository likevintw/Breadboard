using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IotDb.MeasurementManagement.Cpu;

namespace IotDb.MeasurementManagement.Validator
{
    public class GetCpuPageByTimeRequestValidator: AbstractValidator<GetCpuPageByTimeRequest>
    {
        public GetCpuPageByTimeRequestValidator() {
            RuleFor(x => x.StartDateTime)
                .GreaterThan(x => x.EndDateTime)
                .WithMessage("Start date time can't be greater than end date time.");
            RuleFor(x => x.Page.SkipCount)
                .NotEmpty().WithMessage("Skip count can't be null/empty.");
        }
    }
}
