export default function Reducer(state,action) {

    if(action.type === 'ADD_COUNTRY') {
        return {
            countries:[...state.countries, action.country]
        }
            
    }
    if(action.type === 'DELETE_COUNTRY') {
        return {
            countries:state.countries.filter((m,i) => i != action.index)
        };
    }

    return state;

}