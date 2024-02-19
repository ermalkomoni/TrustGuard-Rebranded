import insuranceTypeModel from "./insuranceTypeModel";

export default interface cartItemModel {
  id?: number;
  insuranceTypeId?: number;
  insuranceType?: insuranceTypeModel;
  quantity?: number;
}
