
import insuranceTypeModel from "./insuranceTypeModel";

export default interface orderDetailModel {
  orderDetailId?: number;
  orderHeaderId?: number;
  insuranceTypeId?: number;
  insuranceType?: insuranceTypeModel;
  quantity?: number;
  itemName?: string;
  price?: number;
}
