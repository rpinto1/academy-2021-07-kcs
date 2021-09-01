import { createStore } from "redux";
import Reducer from "./Reducer";

let inicialState = {countries:[]};



const store = createStore(Reducer, inicialState);

export default store;