import React, {useEffect, useState} from 'react';
import { Form, Container, Button } from 'semantic-ui-react';
import UserHeader from '../components/UserHeader';
import { Link } from 'react-router-dom';
import Footer from '../components/Footer';
import Captcha from '../components/SignUp/Captcha';


export default function EditUserView() {

    //all updateable except for emailaddress.
    const [dbUserInfo, setDBUserInfo] = useState({});

    

    const userId  = ((localStorage.getItem('id')) ? localStorage.getItem('id') : sessionStorage.getItem('id'));
    const token = ((localStorage.getItem('token')) ? localStorage.getItem('token') : sessionStorage.getItem('token'));

    const urlGetUser = `http://localhost:3010/api/Users/${userId}`;
    const urlUpdateUser = `http://localhost:3010/api/Update?userId=${userId}`;

    useEffect(() =>
        fetch(urlGetUser, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }).then(res => res.json())
       .then(data => {
        setDBUserInfo({
            Id: data.result.id,
            FirstName: data.result.firstName,
            LastName: data.result.lastName,
            EmailAddress: data.result.emailAddress
        })
      })
      .catch(error => console.log(error)), []);

      

    const handleEditUpdate = () => {
        fetch(urlUpdateUser, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token} `

        },
        body: JSON.stringify(dbUserInfo)
       }).then(res => res.json())
      .catch(error => console.log(error))
      
  };

const handleChange = (event) => {
    const { id, value } = event.target 
    setDBUserInfo(prevState => ({
        ...prevState,
        [id]: value     
    }));
};


console.log(dbUserInfo);




    return (
        <>   
        <UserHeader />

        <Container className= 'formulario'> 
             <h1> {dbUserInfo.FirstName}, let's update your account </h1>
            <Form onSubmit={handleEditUpdate}> 
            <Form.Field>
                <label>First Name</label>
                <input 
                type= 'text' 
                placeholder={dbUserInfo.FirstName}
                onChange = {handleChange}
                id='FirstName'/>
            </Form.Field>
              <Form.Field>
                <label>Last Name</label>
                <input 
                type= 'text' 
                placeholder={dbUserInfo.LastName}
                onChange = {handleChange}
                id='LastName'/>
            </Form.Field>
            <Form.Field>
                <label>Password <span className='tiny'>- Required</span></label>
                <input
                type= 'password' 
                placeholder='Input your current password' 
                onChange = {handleChange}
                id='Password'
                pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}' 
                required/>
            </Form.Field>
            <Form.Field>
                <label>New password <span className='tiny'>- Optional</span></label>      
                <input 
                type ='password'
                placeholder='Write your new password' 
                onChange={handleChange}
                id='NewPassword'/>
            </Form.Field>
            <Form.Field>
                <label>Repeat new password <span className='tiny'> - Not required unless updating your password</span></label>
                <input 
                type ='password'
                placeholder='Rewrite your password' 
                onChange={handleChange}
                id='ConfirmPassword'/>
            </Form.Field>
            <Form.Field>
                <label>E-mail</label>
                <input 
                type= 'email' 
                placeholder='Write your old e-mail address' 
                value = {dbUserInfo.EmailAddress}
                readOnly
                id='EmailAddress'
                required/>
            </Form.Field>
            <Form.Field>
              <Captcha />
       {/*        <div className="container">
                   <div className="form-group">
                       <div></div>
                       <div className="col mt-3">
                            <input 
                             placeholder="Enter Captcha Value" 
                             ref={captchaInputRef}>
                            </input>
                        </div>
                    </div>
                </div>  */}
        
            </Form.Field>
            <Form.Field>
                <Button type='submit'  className='ui small center teal button'>Submit</Button>
                <Link to= '/'><Button type='cancel' className='ui small center red button'>Cancel</Button></Link>
            </Form.Field>
            
            </Form>
           
    
            </Container>
            <Footer /> 
            </>
    )
}
