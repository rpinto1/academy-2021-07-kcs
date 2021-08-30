import React from 'react'
import { Menu } from 'semantic-ui-react'
import './pagination.css'

export default function Pagination({count, page, handlePageClick}) {

    let totalCompanyIndex = eval(Math.ceil(count/10))

    const handleClick = (e) => {
        handlePageClick(e)
    }

     const ReturnPagenation = () => 
    {
        if(totalCompanyIndex == 0){
            return <Menu.Item className="Chosen" onClick={handleClick} as='a'>{0}</Menu.Item>
        }
        else if(totalCompanyIndex <= 6){
            const companyArray = [];
            for (let numberLessFour = 1; numberLessFour <= totalCompanyIndex; numberLessFour++) {
                if(numberLessFour == page) {
                    companyArray.push(<Menu.Item className="Chosen" onClick={handleClick} as='a'>{numberLessFour}</Menu.Item>) 
                    continue;           
                }
                companyArray.push(<Menu.Item onClick={handleClick} as='a'>{numberLessFour}</Menu.Item>)           
            }

            return (                           
                <>
                    {companyArray}
                </>)
        }
        else if(page < 3){
            return (                           
                <>
                    <Menu.Item onClick={handleClick} className="Chosen" as='a'>{page}</Menu.Item>
                    <Menu.Item onClick={handleClick} as='a'>{eval(page + 1)}</Menu.Item>
                    <Menu.Item as='a'>...</Menu.Item>
                    <Menu.Item onClick={handleClick}as='a'>{totalCompanyIndex}</Menu.Item>
                </>)
        }
        else if(page >= totalCompanyIndex -3){
            const companyArray2 = [];
            for (let numberLessFour = 3; numberLessFour >= 0; numberLessFour--) {
                if(totalCompanyIndex - numberLessFour == page) {
                    companyArray2.push(<Menu.Item className="Chosen" onClick={handleClick} as='a'>{totalCompanyIndex- numberLessFour}</Menu.Item>) 
                    continue;           
                }
                companyArray2.push(<Menu.Item onClick={handleClick} as='a'>{totalCompanyIndex - numberLessFour}</Menu.Item>)           
            }

            return(
                <>
                <Menu.Item onClick={handleClick} as='a'>{1}</Menu.Item>
                <Menu.Item as='a'>...</Menu.Item>
                {companyArray2}
                </>
            )
        }
        else{
            return(
                <>
                <Menu.Item onClick={handleClick} as='a'>{1}</Menu.Item>
                <Menu.Item as='a'>...</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{eval(page -1)}</Menu.Item>
                <Menu.Item className="Chosen" onClick={handleClick} as='a'>{page}</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{eval(page +1)}</Menu.Item>
                <Menu.Item as='a'>...</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{totalCompanyIndex}</Menu.Item>
                </>
            )
        }
    }


    return (
            <ReturnPagenation />
    )
}
