import { configureStore } from "@reduxjs/toolkit";
import { insuranceTypeReducer } from "./insuranceTypeSlice";
import {
  authApi,
  insuranceTypeApi,
  orderApi,
  paymentApi,
  shoppingCartApi,
  messageApi,
} from "../../Apis";
import { shoppingCartReducer } from "./shoppingCartSlice";
import { userAuthReducer } from "./userAuthSlice";
const store = configureStore({
  reducer: {
    insuranceTypeStore: insuranceTypeReducer,
    shoppingCartStore: shoppingCartReducer,
    userAuthStore: userAuthReducer,
    [insuranceTypeApi.reducerPath]: insuranceTypeApi.reducer,
    [shoppingCartApi.reducerPath]: shoppingCartApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
    [paymentApi.reducerPath]: paymentApi.reducer,
    [orderApi.reducerPath]: orderApi.reducer,
    [messageApi.reducerPath]: messageApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(insuranceTypeApi.middleware)
      .concat(authApi.middleware)
      .concat(orderApi.middleware)
      .concat(paymentApi.middleware)
      .concat(messageApi.middleware)
      .concat(shoppingCartApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;

export default store;
