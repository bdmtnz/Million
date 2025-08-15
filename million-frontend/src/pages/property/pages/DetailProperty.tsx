import { useLoaderData } from "react-router";

const DetailProperty = () => {
    const data = useLoaderData()

    // useEffect(() => {
    //     alert(JSON.stringify(data))
    // }, []);
    
    return (
        <>
            Property detail
            {data.name}
            <p>{data.price}</p>
        </>
    )
}

export default DetailProperty