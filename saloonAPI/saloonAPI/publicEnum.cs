using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI
{
    public enum AppoinmentType
    {
        HairCut,
        ColouringAndStyling,
        NailTreatment,
        FacialAndSkinCareTreatment,
        WaxingAndHairRemoval,
        Makeup,
        Others
    }

    public enum AppoinmentStatus
    {
        Created,
        Approved,
        Rejected,
        InProgress,
        Completed,
        OnHold,
        Canceled,
        Refunded,
        Paid
    }

    public enum OrderStatus
    {
        Placed,
        Approved,
        Paid,
        Canceled
    }
}
