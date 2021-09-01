import { createStore } from "redux";
import Reducer from "./Reducer";

let inicialState = {countries:[],url:"http://localhost:3010/"};



const store = createStore(Reducer, inicialState);

export default store;