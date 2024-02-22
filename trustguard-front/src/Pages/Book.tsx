import React, { useEffect, useState } from 'react';
import { useCreateBookMutation} from "../Apis/bookApi";
import { inputHelper, toastNotify } from '../Helper';

const bookData = {
    title: "",
    publicationYear: "",
    authorId: "",
  };

function Book(){
    const[bookInputs, setBookInputs] = useState(bookData);

    const handleBookInput = (
        e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
      ) => {
        const tempData = inputHelper(e, bookInputs);
        setBookInputs(tempData);
      };

    const [createBook] = useCreateBookMutation();

    const handleSubmit = async(e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const requestData = {
          title: bookInputs.title,
          publicationYear: bookInputs.publicationYear,
          authorId: bookInputs.authorId,
        };
        const response = await createBook(requestData);
        if (response != null) {
          toastNotify("Message sent successfully!");
          setBookInputs(bookData);
          return;
        }
      }  
    // return(
    //     //your code
    // );
}  

export default Book;