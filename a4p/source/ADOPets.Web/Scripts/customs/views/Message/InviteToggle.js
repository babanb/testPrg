
     $(document).ready(function () {

         $("#friendCall").click(function () {
         
             $('#hdninvitetype').val("Friend");
             $("#ForFriend").css('display', 'block');
             $("#ForVete").css('display', 'none')
             $("#vetCall").prop('checked', false)
           
   
         }); 


         $("#vetCall").click(function () {

             $('#hdninvitetype').val("Veterinarian");
             $("#ForVete").css('display', 'block');
             $("#ForFriend").css('display', 'none')
             $("#friendCall").prop('checked', false)
         
 
         });
     });
  


