import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const messageApi = createApi({
    reducerPath: "messageApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "http://localhost:5131/api/",
    prepareHeaders: (headers: Headers, api) => {
      const token = localStorage.getItem("token");
      token && headers.append("Authorization", "Bearer " + token);
    },
  }),
  tagTypes:["Messages"],
  endpoints: (builder) => ({
    getMessages: builder.query({
        query:() => ({
            url: "messages",
        }),
        providesTags: ["Messages"],
    }),
    createMessage: builder.mutation({
        query: (data) => ({
          url: "messages",
          method: "POST",
          headers: {
            "Content-type": "application/json",
          },
          body: data,
        }),
        invalidatesTags: ["Messages"],
      }),
  })

});

export const { useCreateMessageMutation } = messageApi;
export default messageApi;