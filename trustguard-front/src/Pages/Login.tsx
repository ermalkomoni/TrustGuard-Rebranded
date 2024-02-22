import React, { useState } from "react";
import { useLoginUserMutation } from "../Apis/authApi";
import { inputHelper } from "../Helper";
import { apiResponse, userModel } from "../Interfaces";
import jwt_decode from "jwt-decode";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { setLoggedInUser } from "../Storage/Redux/userAuthSlice";
import { MainLoader } from "../Components/Page/Common";

function Login() {
  const [error, setError] = useState("");
  const [loginUser] = useLoginUserMutation();
  const [loading, setLoading] = useState(false);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [userInput, setUserInput] = useState({
    userName: "",
    password: "",
  });

  const handleUserInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    const tempData = inputHelper(e, userInput);
    setUserInput(tempData);
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);
    const response: apiResponse = await loginUser({
      userName: userInput.userName,
      password: userInput.password,
    });
    if (response.data) {
      const { token } = response.data.result;
      const { fullName, id, email, role }: userModel = jwt_decode(token);
      localStorage.setItem("token", token);
      dispatch(setLoggedInUser({ fullName, id, email, role }));
      navigate("/");
    } else if (response.error) {
      setError(response.error.data.errorMessages[0]);
    }

    setLoading(false);
  };
  return (
    <div className="container-center">
  <div className="container text-center p-5">
    {loading && <MainLoader />}
    <form method="post" onSubmit={handleSubmit}>
      <h1 className="mt-2" style={{ color: "#0b228f" }}>
        Login
      </h1>
      <div className="mt-2">
        <div className="col-sm-6 offset-sm-3 col-xs-12 mt-4">
          <input
            type="text"
            className="form-control"
            placeholder="Enter Username"
            required
            name="userName"
            value={userInput.userName}
            onChange={handleUserInput}
          />
        </div>

        <div className="col-sm-6 offset-sm-3 col-xs-12 mt-4">
          <input
            type="password"
            className="form-control"
            placeholder="Enter Password"
            required
            name="password"
            value={userInput.password}
            onChange={handleUserInput}
          />
        </div>
      </div>

      <div className="mt-4">
        {error && <p className="text-danger">{error}</p>}
        <button
          type="submit"
          className="btn"
          style={{
            width: "200px",
            backgroundColor: "#0b228f",
            color: "#fff",
          }}
        >
          Login
        </button>
      </div>
    </form>
  </div>
</div>

  );
}

export default Login;
