import React, { useEffect, useState } from "react";
import {
  useCreateInsuranceTypeMutation,
  useGetInsuranceTypeByIdQuery,
  useUpdateInsuranceTypeMutation,
} from "../../Apis/insuranceTypeApi";
import { inputHelper, toastNotify } from "../../Helper";
import { useNavigate, useParams } from "react-router-dom";
import { MainLoader } from "../../Components/Page/Common";
import { SD_Categories } from "../../Utility/SD";

const Categories = [
  SD_Categories.INDIVIDUAL,
  SD_Categories.CAR,
  SD_Categories.PROPERTY,
];

const insuranceTypeData = {
  name: "",
  description: "",
  specialTag: "",
  category: Categories[0],
  price: "",
};

function InsuranceTypeUpsert() {
  const { id } = useParams();

  const navigate = useNavigate();
  const [imageToStore, setImageToStore] = useState<any>();
  const [imageToDisplay, setImageToDisplay] = useState<string>("");
  const [insuranceTypeInputs, setInsuranceTypeInputs] = useState(insuranceTypeData);
  const [loading, setLoading] = useState(false);
  const [createInsuranceType] = useCreateInsuranceTypeMutation();
  const [updateInsuranceType] = useUpdateInsuranceTypeMutation();
  const { data } = useGetInsuranceTypeByIdQuery(id);

  useEffect(() => {
    if (data && data.result) {
      const tempData = {
        name: data.result.name,
        description: data.result.description,
        specialTag: data.result.specialTag,
        category: data.result.category,
        price: data.result.price,
      };
      setInsuranceTypeInputs(tempData);
      setImageToDisplay(data.result.image);
    }
  }, [data]);

  const handleInsuranceTypeInput = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement
    >
  ) => {
    const tempData = inputHelper(e, insuranceTypeInputs);
    setInsuranceTypeInputs(tempData);
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files && e.target.files[0];
    if (file) {
      const imgType = file.type.split("/")[1];
      const validImgTypes = ["jpeg", "jpg", "png"];

      const isImageTypeValid = validImgTypes.filter((e) => {
        return e === imgType;
      });

      if (file.size > 1000 * 1024) {
        setImageToStore("");
        toastNotify("File Must be less then 1 MB", "error");
        return;
      } else if (isImageTypeValid.length === 0) {
        setImageToStore("");
        toastNotify("File Must be in jpeg, jpg or png", "error");
        return;
      }

      const reader = new FileReader();
      reader.readAsDataURL(file);
      setImageToStore(file);
      reader.onload = (e) => {
        const imgUrl = e.target?.result as string;
        setImageToDisplay(imgUrl);
      };
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);
    if (!imageToStore && !id) {
      toastNotify("Please upload an image", "error");
      setLoading(false);
      return;
    }

    const formData = new FormData();

    formData.append("Name", insuranceTypeInputs.name);
    formData.append("Description", insuranceTypeInputs.description);
    formData.append("SpecialTag", insuranceTypeInputs.specialTag ?? "");
    formData.append("Category", insuranceTypeInputs.category);
    formData.append("Price", insuranceTypeInputs.price);
    if (imageToDisplay) formData.append("File", imageToStore);

    let response;

    if (id) {
      //update
      formData.append("Id", id);
      response = await updateInsuranceType({ data: formData, id });
      toastNotify("Insurance updated successfully", "success");
    } else {
      //create
      response = await createInsuranceType(formData);
      toastNotify("Insurance created successfully", "success");
    }

    if (response) {
      setLoading(false);
      navigate("/insurancetype/insurancetypelist");
    }

    setLoading(false);
  };

  return (
    <div className="container border mt-5 p-5 bg-light">
      {loading && <MainLoader />}
      <h3 className=" px-2 text-success">
        {id ? "Edit Insurance" : "Add Insurance"}
      </h3>
      <form method="post" encType="multipart/form-data" onSubmit={handleSubmit}>
        <div className="row mt-3">
          <div className="col-md-7">
            <input
              type="text"
              className="form-control"
              placeholder="Enter Name"
              required
              name="name"
              value={insuranceTypeInputs.name}
              onChange={handleInsuranceTypeInput}
            />
            <textarea
              className="form-control mt-3"
              placeholder="Enter Description"
              name="description"
              rows={10}
              value={insuranceTypeInputs.description}
              onChange={handleInsuranceTypeInput}
            ></textarea>
            <input
              type="text"
              className="form-control mt-3"
              placeholder="Enter Special Tag"
              name="specialTag"
              value={insuranceTypeInputs.specialTag}
              onChange={handleInsuranceTypeInput}
            />
            <select
              className="form-control mt-3 form-select"
              placeholder="Enter Category"
              name="category"
              value={insuranceTypeInputs.category}
              onChange={handleInsuranceTypeInput}
            >
              {Categories.map((category) => (
                <option value={category}>{category}</option>
              ))}
            </select>
            <input
              type="number"
              className="form-control mt-3"
              required
              placeholder="Enter Price"
              name="price"
              value={insuranceTypeInputs.price}
              onChange={handleInsuranceTypeInput}
            />
            <input
              type="file"
              onChange={handleFileChange}
              className="form-control mt-3"
            />
            <div className="row">
              <div className="col-6">
                <button
                  type="submit"
                  className="btn btn-success form-control mt-3"
                >
                  {id ? "Update" : "Create"}
                </button>
              </div>
              <div className="col-6">
                <a
                  onClick={() => navigate("/insurancetype/insurancetype")}
                  className="btn btn-secondary form-control mt-3"
                >
                  Back to Insurances
                </a>
              </div>
            </div>
          </div>
          <div className="col-md-5 text-center">
            <img
              src={imageToDisplay}
              style={{ width: "100%", borderRadius: "30px" }}
              alt=""
            />
          </div>
        </div>
      </form>
    </div>
  );
}

export default InsuranceTypeUpsert;
