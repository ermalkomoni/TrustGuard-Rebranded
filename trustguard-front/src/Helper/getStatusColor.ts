import { SD_Status } from "../Utility/SD";

const getStatusColor = (status: SD_Status) => {
  return status === SD_Status.CONFIRMED? "primary"
    : status === SD_Status.PENDING? "secondary"
    : status === SD_Status.CANCELLED? "danger"
    : status === SD_Status.COMPLETED? "success"
    : status === SD_Status.IN_PROGRESS? "info"
    : status === SD_Status.APPROVED && "warning";
};

export default getStatusColor;
