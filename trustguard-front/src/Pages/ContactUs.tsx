import React, { useEffect, useState } from 'react';
import { useCreateMessageMutation} from "../Apis/messageApi";
import { inputHelper, toastNotify } from '../Helper';

const contactUsData = {
  name: "",
  email: "",
  message: "",
};

function ContactUs(){
  const [contactUsInputs, setContactUsInputs] = useState(contactUsData);

  const handleContactUsInput = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
  ) => {
    const tempData = inputHelper(e, contactUsInputs);
    setContactUsInputs(tempData);
  };

  const [createMessage] = useCreateMessageMutation();

  const handleSubmit = async(e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const requestData = {
      name: contactUsInputs.name,
      email: contactUsInputs.email,
      message: contactUsInputs.message,
    };
    const response = await createMessage(requestData);
    if (response != null) {
      toastNotify("Message sent successfully!");
      setContactUsInputs(contactUsData);
      return;
    }
  }

  return(
      <div className="form-area">
      <div className="container">
          <div className="row single-form g-0">
              <div className="col-sm-12 col-lg-6">
                  <div className="left">
                      <h2>Contact Us</h2>
                      <br />
                      <h3>Tel:</h3>
                      <p>049 333 444</p>
                      <br />
                      <h3>Email:</h3>
                      <p>trustguard.ks@gmail.com</p>
                  </div>
              </div>
              <div className="col-sm-12 col-lg-6">
                  <div className="right">
                     <i className="fa fa-caret-left"></i>
                      <form method='post' onSubmit={handleSubmit}>                       
                        <div className="mb-3">
                          <label htmlFor="exampleInputEmail1" className="form-label">Your Name</label>
                          <input
                            type="text"
                            className="form-control"
                            id="exampleInputEmail1"
                            // aria-describedby="emailHelp"
                            required
                            name='name'
                            value={contactUsInputs.name}
                            onChange={handleContactUsInput}
                          />
                        </div>
                        <div className="mb-3">
                          <label htmlFor="exampleInputEmail1" className="form-label">Email address</label>
                          <input type="email"
                           className="form-control"
                            id="exampleInputEmail1"
                            //  aria-describedby="emailHelp"
                            required
                            name='email'
                            value={contactUsInputs.email}
                            onChange={handleContactUsInput}
                          />
                        </div>
                        <div className="mb-3">
                          <label htmlFor="exampleInputPassword1" className="form-label">Message</label>
                          <textarea
                            className="form-control"
                            id="exampleInputPassword1"
                            required
                            name='message'
                            rows={10}
                            value={contactUsInputs.message}
                            onChange={handleContactUsInput}
                          ></textarea>
                        </div>
                        <button
                         type="submit" 
                         className="btn btn-primary">Submit</button>
                      </form>
                  </div>
              </div>
          </div>
      </div>
  </div> 
);
}

export default ContactUs;