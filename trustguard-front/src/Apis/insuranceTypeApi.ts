import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const insuranceTypeApi = createApi({
  reducerPath: "insuranceTypeApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "http://localhost:5131/api/",
    prepareHeaders: (headers: Headers, api) => {
      const token = localStorage.getItem("token");
      token && headers.append("Authorization", "Bearer " + token);
    },
  }),
  tagTypes: ["InsuranceTypes"],
  endpoints: (builder) => ({
    getInsuranceTypes: builder.query({
      query: () => ({
        url: "InsuranceType",
      }),
      providesTags: ["InsuranceTypes"],
    }),
    getInsuranceTypeById: builder.query({
      query: (id) => ({
        url: `InsuranceType/${id}`,
      }),
      providesTags: ["InsuranceTypes"],
    }),
    createInsuranceType: builder.mutation({
      query: (data) => ({
        url: "InsuranceType",
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["InsuranceTypes"],
    }),
    updateInsuranceType: builder.mutation({
      query: ({ data, id }) => ({
        url: "InsuranceType/" + id,
        method: "PUT",
        body: data,
      }),
      invalidatesTags: ["InsuranceTypes"],
    }),
    deleteInsuranceType: builder.mutation({
      query: (id) => ({
        url: "InsuranceType/" + id,
        method: "DELETE",
      }),
      invalidatesTags: ["InsuranceTypes"],
    }),
  }),
});

export const {
  useGetInsuranceTypesQuery,
  useGetInsuranceTypeByIdQuery,
  useCreateInsuranceTypeMutation,
  useUpdateInsuranceTypeMutation,
  useDeleteInsuranceTypeMutation,
} = insuranceTypeApi;
export default insuranceTypeApi;
