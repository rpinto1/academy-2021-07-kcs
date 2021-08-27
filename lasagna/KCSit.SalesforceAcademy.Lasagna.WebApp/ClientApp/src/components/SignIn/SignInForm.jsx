import React, {useState} from 'react';
import { Button, Container, Form, Checkbox} from 'semantic-ui-react';
import axios from 'axios';

export default function SignInForm() {

    const [user, setUser] = useState({
      EmailAddress: '',
      Password: ''
    });

    
    const [isRobot, setIsRobot] = useState('true');

    const submitUser = () => {
        if (isRobot){
            return (
                    <div class="ui negative message">
                    <i class="close icon"></i>
                        <div class="header">
                            WE ARE SORRY!
                        </div>
                        <p>Our website is only available to humans, not robots.</p>
                        </div>

            )
        };
        
        axios.post("api/user/authenticate", user)
             .catch ((error) => {console.log(error);});
             console.log(user);

    };
  
    

    
      return (
        <Container className= 'form'>
          <h1>Sign in to your account</h1>
          <Form onSubmit= {console.log('hola')}>
         <Form.Field> 
          <label htmlFor="username">Username: </label>
          <input
            type="text"
            value={user.EmailAddress}
            placeholder="Enter your username"
            onChange={({ target}) => setUser((prevState)=> ({...prevState, keyword: target.value,}))}
          />
          </Form.Field>  

          <Form.Field> 
            <label htmlFor="password">Password: </label>
            <input
              type="password"
              value={user.Password}
              placeholder="Enter your password"
              onChange={({ target}) => setUser((prevState)=> ({...prevState, keyword: target.value,}))}
            />
         </Form.Field> 
         <Form.Field>
            <Checkbox type='checkbox' 
            name='isRobot' 
            label='Are you a robot?' 
            value= {isRobot}
            onChange= {setIsRobot('false')}
            required/>        
            </Form.Field>
          <Button type="submit">Sign in</Button>
        </Form>
        </Container>

      );
    
    

};
