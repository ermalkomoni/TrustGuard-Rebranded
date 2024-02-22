import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const bookApi = createApi({
    reducerPath: "bookApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "http://localhost:5131/api/",
    prepareHeaders: (headers: Headers, api) => {
      const token = localStorage.getItem("token");
      token && headers.append("Authorization", "Bearer " + token);
    },
  }),
  tagTypes:["Books"],
  endpoints: (builder) => ({
    getBooks: builder.query({
        query:() => ({
            url: "books",
        }),
        providesTags: ["Books"],
    }),
    createBook: builder.mutation({
        query: (data) => ({
          url: "books",
          method: "POST",
          headers: {
            "Content-type": "application/json",
          },
          body: data,
        }),
        invalidatesTags: ["Books"],
      }),
    updateBook: builder.mutation({
    query: ({ data, id }) => ({
        url: "Book/" + id,
        method: "PUT",
        body: data,
    }),
    invalidatesTags: ["Books"],
    }),
  })

  //post
  //get
  //update
  //get by name/viti publikimit

});

export const { useCreateBookMutation } = bookApi;
export default bookApi;