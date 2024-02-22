import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  insuranceType: [],
  search: "",
};

export const insuranceTypeSlice = createSlice({
  name: "InsuranceType",
  initialState: initialState,
  reducers: {
    setInsuranceType: (state, action) => {
      state.insuranceType = action.payload;
    },
    setSearchItem: (state, action) => {
      state.search = action.payload;
    },
  },
});

export const { setInsuranceType, setSearchItem } = insuranceTypeSlice.actions;
export const insuranceTypeReducer = insuranceTypeSlice.reducer;
